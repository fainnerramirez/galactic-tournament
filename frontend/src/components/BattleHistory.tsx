import { Table, Text } from '@mantine/core'
import type { Battle } from '../types'

interface Props {
  battles: Battle[]
}

export default function BattleHistory({ battles }: Props) {
  if (battles.length === 0) {
    return <Text c="dimmed">Aún no hay combates.</Text>
  }

  return (
    <Table striped highlightOnHover withTableBorder>
      <Table.Thead>
        <Table.Tr>
          <Table.Th>Fecha</Table.Th>
          <Table.Th>Combate</Table.Th>
          <Table.Th>Ganador</Table.Th>
        </Table.Tr>
      </Table.Thead>
      <Table.Tbody>
        {battles.map((b) => (
          <Table.Tr key={b.id}>
            <Table.Td>{new Date(b.foughtAt).toLocaleString()}</Table.Td>
            <Table.Td>{b.speciesAName} vs {b.speciesBName}</Table.Td>
            <Table.Td>{b.winnerName}</Table.Td>
          </Table.Tr>
        ))}
      </Table.Tbody>
    </Table>
  )
}