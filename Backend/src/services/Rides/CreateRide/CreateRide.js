import axios from 'axios';

export const createRide = async (rideData) => {
    try {
        const response = await axios.post(
            'https://localhost:44325/api/rides/createRide', 
            rideData, // Pass the object directly, no need for JSON.stringify
            {
                headers: {
                    'Content-Type': 'application/json', // This header is fine
                }
            }
        );
        return response.data;
    } catch (error) {
        console.error('Error creating ride:', error);
        throw error;
    }
};
