import axios from 'axios';

export const getPreviousRides = async (token) => {
    try {
        const token = localStorage.getItem('token'); // Ensure this key is correct
        if (!token) {
            throw new Error('No token found in local storage');
        }
        const trueToken = JSON.parse(token).accessToken;    //getting the JWT from localstorage accessToken: "jwtValue"
        const response = await axios.get('https://localhost:44325/api/rides/getPreviousRides', {
            headers: {
                Authorization: `Bearer ${trueToken}`,
            },
        });
        return response.data;
    } catch (error) {
        console.error('Error fetching previous rides:', error);
        throw error;
    }
};
