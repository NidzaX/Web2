import axios from 'axios';

export const reserveRide = async (rideId, token) => {
    try {
        const response = await axios.post('https://localhost:44325/api/rides/reserveRide', {
            rideId: rideId,
        }, {
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
        });

        console.log('API Response:', response.data); // Log the entire response

        // Extract values from nested objects
        const waitingTimeValue = response.data.waitingTime?.value;
        const predictedTimeValue = response.data.predictedTime?.value;

        console.log('Extracted waitingTime:', waitingTimeValue);
        console.log('Extracted predictedTime:', predictedTimeValue);

        return {
            waitingTime: waitingTimeValue,
            predictedTime: predictedTimeValue,
        };
    } catch (error) {
        console.error('Error reserving ride:', error.response ? error.response.data : error);
        throw error;
    }
};
