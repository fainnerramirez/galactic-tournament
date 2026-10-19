import { Badge, Table, Text } from '@mantine/core'
import type { RankingEntry } from '../types'

interface Props {
  ranking: RankingEntry[]
}

const medals = ['🥇', '🥈', '🥉']

export default function RankingTable({ ranking }: Props) {
  if (ranking.length === 0) {
    return <Text c="dimmed">Aún no hay especies registradas.</Text>
  }

  return (
    <Table striped highlightOnHover withTableBorder>
      <Table.Thead>
        <Table.Tr>
          <Table.Th>#</Table.Th>
          <Table.Th>Especie</Table.Th>
          <Table.Th>Poder</Table.Th>
          <Table.Th>Victorias</Table.Th>
          <Table.Th>Combates</Table.Th>
        </Table.Tr>
      </Table.Thead>
      <Table.Tbody>
        {ranking.map((r) => (
          <Table.Tr key={r.speciesId}>
            <Table.Td>{medals[r.position - 1] ?? r.position}</Table.Td>
            <Table.Td fw={r.position === 1 ? 700 : undefined}>{r.name}</Table.Td>
            <Table.Td>{r.powerLevel}</Table.Td>
            <Table.Td>
              <Badge variant="light" color={r.wins > 0 ? 'green' : 'gray'}>{r.wins}</Badge>
            </Table.Td>
            <Table.Td>{r.battlesPlayed}</Table.Td>
          </Table.Tr>
        ))}
      </Table.Tbody>
    </Table>
  )
}