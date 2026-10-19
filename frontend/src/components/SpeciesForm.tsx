import { useState, type FormEvent } from 'react'
import { Button, Group, NumberInput, Paper, TextInput } from '@mantine/core'
import type { CreateSpeciesRequest } from '../types'
import { toast } from 'sonner'
import { getErrorMessage } from '../utils/error'

interface Props {
  onCreate: (data: CreateSpeciesRequest) => Promise<void>
}

export default function SpeciesForm({ onCreate }: Props) {
  const [name, setName] = useState('')
  const [powerLevel, setPowerLevel] = useState<number | string>(0)
  const [specialAbility, setSpecialAbility] = useState('')
  const [loading, setLoading] = useState(false)

  async function handleSubmit(e: FormEvent) {
    e.preventDefault()
    setLoading(true)
    try {
      await onCreate({
        name: name.trim(),
        powerLevel: Number(powerLevel),
        specialAbility: specialAbility.trim(),
      })
      setName('')
      setPowerLevel(0)
      setSpecialAbility('')
    } catch (error) {
      toast.error(getErrorMessage(error, 'No se pudo registrar la especie'))
    } finally {
      setLoading(false)
    }
  }

  return (
    <Paper withBorder p="md" mb="lg">
      <form onSubmit={handleSubmit}>
        <Group align="flex-end" grow>
          <TextInput
            label="Nombre"
            placeholder="Elfo"
            value={name}
            onChange={(e) => setName(e.currentTarget.value)}
            required
          />
          <NumberInput
            label="Nivel de poder"
            min={0}
            allowDecimal={false}
            allowNegative={false}
            value={powerLevel}
            onChange={setPowerLevel}
            required
          />
          <TextInput
            label="Habilidad especial"
            placeholder="Puntería perfecta"
            value={specialAbility}
            onChange={(e) => setSpecialAbility(e.currentTarget.value)}
            required
          />
          <Button type="submit" loading={loading} style={{ flexGrow: 0 }}>
            Registrar
          </Button>
        </Group>
      </form>
    </Paper>
  )
}