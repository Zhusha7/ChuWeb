// Use HTTPS port from our ASP.NET Core backend
const API_BASE_URL = 'https://localhost:7001/api';

// Fallback to HTTP if HTTPS fails
const API_FALLBACK_URL = 'http://localhost:5001/api';

export const api = {
    async getHello() {
        try {
            try {
                // First try HTTPS
                const response = await fetch(`${API_BASE_URL}/hello`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    mode: 'cors',
                    credentials: 'omit'
                });
                
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                
                return response.json();
            } catch (error) {
                // If HTTPS fails, try HTTP fallback
                console.warn('HTTPS request failed, trying HTTP fallback', error);
                
                const fallbackResponse = await fetch(`${API_FALLBACK_URL}/hello`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    mode: 'cors',
                    credentials: 'omit'
                });
                
                if (!fallbackResponse.ok) {
                    throw new Error(`HTTP error! status: ${fallbackResponse.status}`);
                }
                
                return fallbackResponse.json();
            }
        } catch (error) {
            console.error('API Error:', error);
            throw error;
        }
    }
}; 