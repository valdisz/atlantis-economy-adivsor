import { action, makeObservable, observable, runInAction, transaction } from 'mobx'
import { CLIENT } from '../client'
import { GameListItemFragment, GetGamesQuery, GetGames } from '../schema'
import { CreateGame, CreateGameMutation, CreateGameMutationVariables } from '../schema'
import { JoinGame, JoinGameMutation, JoinGameMutationVariables } from '../schema'
import { DeleteGame, DeleteGameMutation, DeleteGameMutationVariables } from '../schema'
import { NewGameStore } from './new-game-store'

export class HomeStore {
    constructor() {
        makeObservable(this)
    }

    readonly games = observable<GameListItemFragment>([]);

    load = (games?: GameListItemFragment[]) => {
        if (games) {
            runInAction(() => {
                this.games.replace(games);
            })
            return
        }

        CLIENT.query<GetGamesQuery>({
            query: GetGames
        }).then(response => {
            runInAction(() => {
                this.games.replace(response.data.games);
            });
        });
    };

    readonly newGame = new NewGameStore()

    confirmNewGame = async () => {
        const response = await CLIENT.mutate<CreateGameMutation, CreateGameMutationVariables>({
            mutation: CreateGame,
            variables: {
                name: this.newGame.name
            }
        });

        transaction(() => {
            this.games.push(response.data.createGame);
            this.newGame.cancel();
        });
    };

    private fileUpload: HTMLInputElement = null
    @action setFileUpload = (ref: HTMLInputElement) => {
        this.fileUpload = ref
    }

    @observable uploading = false

    uploadPlayerId: string
    uploadAction: 'report' | 'map' | 'ruleset' = 'report'

    triggerUploadReport = (playerId: string) => {
        this.uploadPlayerId = playerId
        this.uploadAction = 'report'

        this.fileUpload?.click()
    }

    triggerImportMap = (playerId: string) => {
        this.uploadPlayerId = playerId
        this.uploadAction = 'map'

        this.fileUpload?.click()
    }

    triggerRuleset = (gameId: string) => {
        this.uploadPlayerId = gameId
        this.uploadAction = 'ruleset'

        this.fileUpload?.click()
    }

    @action uploadFile = async (event: React.ChangeEvent<HTMLInputElement>) => {
        this.uploading = true

        const reports = new FormData()
        for (const f of Array.from(event.target.files)) {
            reports.append('report', f)
        }

        await fetch(`/api/${this.uploadPlayerId}/${this.uploadAction}`, {
            method: 'POSt',
            body: reports,
            credentials: 'include'
        })
        event.target.value = null;

        this.load()
        runInAction(() => this.uploading = false)
    }

    joinGame = async (gameId: string) => {
        const response = await CLIENT.mutate<JoinGameMutation, JoinGameMutationVariables>({
            mutation: JoinGame,
            variables: { gameId }
        });

        this.load();
    }

    deleteGame = async (gameId: string) => {
        const response = await CLIENT.mutate<DeleteGameMutation, DeleteGameMutationVariables>({
            mutation: DeleteGame,
            variables: { gameId }
        });

        this.load(response.data.deleteGame);
    }
}
