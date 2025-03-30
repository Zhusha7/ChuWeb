import { useState, useEffect } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { api } from './services/api'

function App() {
  const [message, setMessage] = useState<string>('Loading...')
  const [error, setError] = useState<string>('')

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await api.getHello()
        setMessage(data.message)
      } catch (err) {
        const errorMessage = err instanceof Error ? err.message : 'Failed to fetch data from API'
        setError(errorMessage)
        console.error('Error details:', err)
      }
    }

    fetchData()
  }, [])

  return (
    <div className="App">
      <header className="App-header">
        <h1>Welcome to ChuWeb</h1>
        {error ? (
          <div style={{ color: 'red', textAlign: 'center' }}>
            <p>Error: {error}</p>
            <p>Please check if the backend API is running at https://localhost:7001</p>
          </div>
        ) : (
          <p>{message}</p>
        )}
      </header>
    </div>
  )
}

export default App
