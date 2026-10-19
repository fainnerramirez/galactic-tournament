import { Paper, Text } from '@mantine/core'
import type { Battle } from '../types'

interface Props {
  battle: Battle | null
}

export default function BattleResult({ battle }: Props) {
  if (!battle) return null

  return (
    <Paper withBorder p="md" mb="lg" ta="center">
      <Text size="lg">
        {battle.speciesAName} vs {battle.speciesBName}
      </Text>
      <Text size="xl" fw={700} mt={4}>
        🏆 Gana {battle.winnerName}
      </Text>
    </Paper>
  )
}