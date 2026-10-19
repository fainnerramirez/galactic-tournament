import { useState } from 'react'
import { Button, Group, Paper, Select, Text } from '@mantine/core'
import type { Species } from '../types'

interface Props {
  species: Species[]
  loading: boolean
  onFight: (aId: number, bId: number) => void
  onRandom: () => void
}

export default function BattleForm({ species, loading, onFight, onRandom }: Props) {
  const [aId, setAId] = useState<string | null>(null)
  const [bId, setBId] = useState<string | null>(null)

  const options = species.map((s) => ({
    value: String(s.id),
    label: `${s.name} (${s.powerLevel})`,
  }))

  const sameSpecies = aId !== null && aId === bId
  const canFight = aId !== null && bId !== null && !sameSpecies

  return (
    <Paper withBorder p="md" mb="lg">
      <Group align="flex-end" grow>
        <Select
          label="Especie A"
          placeholder="Elige una especie"
          data={options}
          value={aId}
          onChange={setAId}
          searchable
        />
        <Text ta="center" style={{ flexGrow: 0 }} pb={6}>⚔️</Text>
        <Select
          label="Especie B"
          placeholder="Elige una especie"
          data={options}
          value={bId}
          onChange={setBId}
          searchable
          error={sameSpecies ? 'Elige una especie distinta' : undefined}
        />
      </Group>

      <Group mt="md">
        <Button
          disabled={!canFight}
          loading={loading}
          onClick={() => onFight(Number(aId), Number(bId))}
        >
          Combatir
        </Button>
        <Button variant="light" loading={loading} disabled={species.length < 2} onClick={onRandom}>
          🎲 Combate aleatorio
        </Button>
      </Group>
    </Paper>
  )
}