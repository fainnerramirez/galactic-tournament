import { Table, Text } from '@mantine/core'
import type { Species } from '../types'

interface Props {
  species: Species[]
}

export default function SpeciesList({ species }: Props) {
  if (species.length === 0) {
    return <Text c="dimmed">Aún no hay especies registradas.</Text>
  }

  return (
    <Table striped highlightOnHover withTableBorder>
      <Table.Thead>
        <Table.Tr>
          <Table.Th>Nombre</Table.Th>
          <Table.Th>Poder</Table.Th>
          <Table.Th>Habilidad especial</Table.Th>
        </Table.Tr>
      </Table.Thead>
      <Table.Tbody>
        {species.map((s) => (
          <Table.Tr key={s.id}>
            <Table.Td>{s.name}</Table.Td>
            <Table.Td>{s.powerLevel}</Table.Td>
            <Table.Td>{s.specialAbility}</Table.Td>
          </Table.Tr>
        ))}
      </Table.Tbody>
    </Table>
  )
}