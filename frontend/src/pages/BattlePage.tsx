import { useCallback, useEffect, useState } from 'react'
import { Title } from '@mantine/core'
import { toast } from 'sonner'
import { api } from '../api/client'
import type { Battle, Species } from '../types'
import BattleForm from '../components/BattleForm'
import BattleResult from '../components/BattleResult'
import BattleHistory from '../components/BattleHistory'
import { getErrorMessage } from '../utils/error'

export default function BattlePage() {
  const [species, setSpecies] = useState<Species[]>([])
  const [battles, setBattles] = useState<Battle[]>([])
  const [result, setResult] = useState<Battle | null>(null)
  const [fighting, setFighting] = useState(false)

  const load = useCallback(async () => {
    try {
      const [s, b] = await Promise.all([api.getSpecies(), api.getBattles()])
      setSpecies(s)
      setBattles(b)
    } catch (e) {
      toast.error('No se pudieron cargar los datos', { description: getErrorMessage(e) })
    }
  }, [])

  useEffect(() => { load() }, [load])

  async function run(action: () => Promise<Battle>) {
    setFighting(true)
    try {
      const battle = await action()
      setResult(battle)
      toast.success(`🏆 Gana ${battle.winnerName}`)
      setBattles(await api.getBattles())
    } catch (e) {
      setResult(null)
      toast.error('No se pudo realizar el combate', { description: getErrorMessage(e) })
    } finally {
      setFighting(false)
    }
  }

  return (
    <>
      <Title order={3} mb="sm">Iniciar combate</Title>
      <BattleForm
        species={species}
        loading={fighting}
        onFight={(a, b) => run(() => api.combat(a, b))}
        onRandom={() => run(api.combatRandom)}
      />
      <BattleResult battle={result} />
      <Title order={3} mb="sm">Historial ({battles.length})</Title>
      <BattleHistory battles={battles} />
    </>
  )
}