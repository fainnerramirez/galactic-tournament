import axios, { AxiosError } from 'axios'
import type { Battle, CreateSpeciesRequest, RankingEntry, Species } from '../types'

const http = axios.create({
    baseURL: '/api',
    headers: { 'Content-Type': 'application/json' },
})

http.interceptors.response.use(
    (response) => response,
    (error: AxiosError<{ detail?: string; title?: string }>) => {
        const message =
            error.response?.data?.detail ??
            error.response?.data?.title ??
            (error.request
                ? 'No se pudo conectar con el servidor.'
                : error.message)
        return Promise.reject(new Error(message))
    },
)

export const api = {
    getSpecies: async () => {
        const { data: species } = await http.get<Species[]>('/species');
        return species;
    },

    createSpecies: async (data: CreateSpeciesRequest) => {
        const { data: species } = await http.post<Species>('/species', data);
        return species;
    },

    combat: async (speciesAId: number, speciesBId: number) => {
        const { data: battle } = await http.post<Battle>('/battles', { speciesAId, speciesBId });
        return battle;
    },

    combatRandom: async () => {
        const { data: battle } = await http.post<Battle>('/battles/random');
        return battle;
    },

    getBattles: async () => {
        const { data: battles } = await http.get<Battle[]>('/battles');
        return battles;
    },

    getRanking: async () => {
        const { data: ranking } = await http.get<RankingEntry[]>('/ranking');
        return ranking;
    },
}