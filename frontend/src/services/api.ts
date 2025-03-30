const API_BASE_URL = 'http://localhost:7001/api';

export const api = {
    async getHello() {
        try {
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
            console.error('API Error:', error);
            throw error;
        }
    }
}; 