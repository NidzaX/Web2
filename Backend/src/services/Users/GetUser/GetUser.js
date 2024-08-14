import axios from 'axios';
import User from '../../../components/model/User';

const API_URL = "https://localhost:44325/api/users/getUserData";

// `${process.env.REACT_APP_API_URL}/api/users/changePassword`
export const getUserData = async (userId) => {
    try {
        const response = await axios.get(`${API_URL}/${userId}`);
        const userData = response.data;

        // Check if userData properties are present and correctly formatted
        if (!userData) {
            throw new Error('User data is null or undefined');
        }

        // Map the API response to the User model
        const user = new User(
            userData.id,
            userData.firstName,
            userData.lastName,
            userData.email,
            userData.username,
            null, // Assuming you do not receive the password from the API for security reasons
            userData.address,
            new Date(userData.birthday), // Ensure this is a Date object
            userData.userType,
            userData.file,
            userData.verified
        );

        return user;
    } catch (error) {
        console.error("Error fetching user data:", error);
        throw error;
    }
};
