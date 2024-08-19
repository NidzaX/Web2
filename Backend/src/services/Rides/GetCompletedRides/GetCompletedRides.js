import axios from 'axios';

export const getCompletedRides = async () => {
    try {
        const token = localStorage.getItem('token');
        if (!token) {
            throw new Error('No token found in local storage');
        }
        const trueToken = JSON.parse(token).accessToken;
        const response = await axios.get('https://localhost:44325/api/rides/getCompletedRides', {
            headers: {
                'Authorization': `Bearer ${trueToken}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('Error fetching completed rides:', error);
        throw error;
    }
};
