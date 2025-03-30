import { useState, useEffect } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { api } from './services/api'

function App() {
  const [message, setMessage] = useState<string>('Loading...')
  const [error, setError] = useState<string>('')
  const [isLoading, setIsLoading] = useState<boolean>(true)

  useEffect(() => {
    const fetchData = async () => {
      try {
        setIsLoading(true)
        const data = await api.getHello()
        setMessage(data.message)
        setError('')
      } catch (err) {
        const errorMessage = err instanceof Error ? err.message : 'Failed to fetch data from API'
        setError(errorMessage)
        console.error('Error details:', err)
      } finally {
        setIsLoading(false)
      }
    }

    fetchData()
  }, [])

  return (
    <div className="App">
      <header className="App-header">
        <div style={{ display: 'flex' }}>
          <a href="https://vitejs.dev" target="_blank">
            <img src={viteLogo} className="logo" alt="Vite logo" />
          </a>
          <a href="https://react.dev" target="_blank">
            <img src={reactLogo} className="logo react" alt="React logo" />
          </a>
        </div>
        <h1>ChuWeb</h1>
        <h2>React + Vite + ASP.NET Core</h2>
        
        <div className="card">
          {isLoading ? (
            <p>Loading API data...</p>
          ) : error ? (
            <div className="api-error">
              <p>Error: {error}</p>
              <p>Please check if the backend API is running:</p>
              <ul style={{ listStyle: 'none', padding: 0 }}>
                <li>HTTPS: https://localhost:7001</li>
                <li>HTTP: http://localhost:5001</li>
              </ul>
              <button onClick={() => window.location.reload()}>Retry</button>
            </div>
          ) : (
            <div className="api-response">
              <h3>API Response:</h3>
              <p>{message}</p>
            </div>
          )}
        </div>
        
        <p className="read-the-docs">
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </header>
    </div>
  )
}

export default App
