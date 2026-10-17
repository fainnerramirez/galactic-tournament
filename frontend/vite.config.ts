import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: {
      '/api': 'http://localhost:5226',
    },
  },
  build: {
    // El build cae directo en el wwwroot de la API
    outDir: '../backend/TorneoGalactico.Api/wwwroot',
    emptyOutDir: true,
  },
})