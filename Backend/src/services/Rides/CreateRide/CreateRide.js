import axios from 'axios';

export const createRide = async (rideData) => {
    try {
        const token = localStorage.getItem('token'); // Ensure this key is correct

        if (!token) {
            throw new Error('No token found in local storage');
        }

        const trueToken = JSON.parse(token).accessToken;    //getting the JWT from localstorage accessToken: "jwtValue"
        const response = await axios.post(
            'https://localhost:44325/api/rides/createRide',
            rideData,
            {
                headers: {
                    'Content-Type': 'application/json',
                   'Authorization': `Bearer ${trueToken}` // Token should be set here
                }
            }
        );

        return response.data;
    } catch (error) {
        console.error('Error creating ride:', error.response ? error.response.data : error.message);
        throw error;
    }
};
