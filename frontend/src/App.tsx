import { Container, Tabs, Title } from '@mantine/core'
import SpeciesPage from './pages/SpeciesPage'
import BattlePage from './pages/BattlePage'
import RankingPage from './pages/RankingPage'

export default function App() {
  return (
    <Container size="md" py="lg">
      <Title order={1} mb="md">🌌 Torneo Galáctico - Morning Value</Title>
      <Tabs defaultValue="species" keepMounted={false}>
        <Tabs.List mb="md">
          <Tabs.Tab value="species">Especies</Tabs.Tab>
          <Tabs.Tab value="battle">Combate</Tabs.Tab>
          <Tabs.Tab value="ranking">Ranking</Tabs.Tab>
        </Tabs.List>

        <Tabs.Panel value="species"><SpeciesPage /></Tabs.Panel>
        <Tabs.Panel value="battle"><BattlePage /></Tabs.Panel>
        <Tabs.Panel value="ranking"><RankingPage /></Tabs.Panel>
      </Tabs>
    </Container>
  )
}