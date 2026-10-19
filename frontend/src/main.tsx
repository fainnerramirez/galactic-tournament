import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { MantineProvider } from '@mantine/core'
import { Toaster } from "sonner";
import '@mantine/core/styles.css'
import '@mantine/notifications/styles.css'
import App from './App'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <MantineProvider defaultColorScheme="dark">
      <Toaster position='top-right' />
      <App />
    </MantineProvider>
  </StrictMode>,
)