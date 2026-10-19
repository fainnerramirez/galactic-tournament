import { useEffect, useState } from 'react'
import { Loader, Title } from '@mantine/core'
import { toast } from 'sonner'
import { api } from '../api/client'
import type { RankingEntry } from '../types'
import RankingTable from '../components/RankingTable'
import { getErrorMessage } from '../utils/error'

export default function RankingPage() {
  const [ranking, setRanking] = useState<RankingEntry[]>([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    api.getRanking()
      .then(setRanking)
      .catch((e) => toast.error('No se pudo cargar el ranking', { description: getErrorMessage(e) }))
      .finally(() => setLoading(false))
  }, [])

  return (
    <>
      <Title order={3} mb="sm">Ranking por victorias</Title>
      {loading ? <Loader /> : <RankingTable ranking={ranking} />}
    </>
  )
}