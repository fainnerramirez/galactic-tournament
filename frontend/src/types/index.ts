export interface Species {
  id: number
  name: string
  powerLevel: number
  specialAbility: string
}

export type CreateSpeciesRequest = Omit<Species, 'id'>

export interface Battle {
  id: number
  speciesAId: number
  speciesAName: string
  speciesBId: number
  speciesBName: string
  winnerId: number
  winnerName: string
  foughtAt: string
}

export interface RankingEntry {
  position: number
  speciesId: number
  name: string
  powerLevel: number
  wins: number
  battlesPlayed: number
}