import { useCallback, useEffect, useState } from 'react'
import { Loader, Title } from '@mantine/core'
import { api } from '../api/client'
import type { CreateSpeciesRequest, Species } from '../types'
import SpeciesForm from '../components/SpeciesForm'
import SpeciesList from '../components/SpeciesList'
import { toast } from 'sonner'

export default function SpeciesPage() {
  const [species, setSpecies] = useState<Species[]>([])
  const [loading, setLoading] = useState(true)

  const load = useCallback(async () => {
    try {
      setSpecies(await api.getSpecies())
    } catch (e) {
     toast.error('Error', { description: (e as Error).message })
    } finally {
      setLoading(false)
    }
  }, [])

  useEffect(() => { load() }, [load])

  async function handleCreate(data: CreateSpeciesRequest) {
    try {
      const created = await api.createSpecies(data)
      toast.success(`Especie "${created.name}" registrada`)
      await load()
    } catch (e) {
      toast.error('No se pudo registrar', { description: (e as Error).message })
      throw e
    }
  }

  return (
    <>
      <Title order={3} mb="sm">Registrar especie</Title>
      <SpeciesForm onCreate={handleCreate} />
      <Title order={3} mb="sm">Especies registradas ({species.length})</Title>
      {loading ? <Loader /> : <SpeciesList species={species} />}
    </>
  )
}