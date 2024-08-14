import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { addReview } from '../../services/Reviews/AddReview/AddReview'; // Update the path to where your service file is located
import axios from 'axios'; 

const ReviewRide = () => {
    const [rideId, setRideId] = useState(null);
    const [driverId, setDriverId] = useState(null); // Add driverId state
    const [userEmail, setUserEmail] = useState('');
    const [rating, setRating] = useState(0);
    const [comment, setComment] = useState('');
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(null);

    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);

    useEffect(() => {
        const rideId = queryParams.get('rideId');
        setRideId(rideId);

        // Fetch user email from local storage or API if needed
        const storedEmail = localStorage.getItem('userEmail');
        setUserEmail(storedEmail || 'example@example.com'); // Placeholder if no email is found

        // Logic to fetch the driverId associated with the rideId
        const fetchDriverId = async (rideId) => {
            try {
                // Implement your logic here to fetch the driverId based on rideId
                // For example, you might make an API call to get ride details, including the driverId
                const response = await axios.get(`https://localhost:44325/api/rides/${rideId}`);
                setDriverId(response.data.driverId); // Assuming driverId is in the response data
            } catch (error) {
                console.error('Error fetching driver ID:', error);
                setError('Failed to fetch driver information. Please try again.');
            }
        };

        fetchDriverId(rideId);
    }, [location.search]);

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            // Get the token from local storage
            const token = localStorage.getItem('token');

            // Submit review to API
            await addReview(userEmail, driverId, rating, comment, token);
            setSuccess('Review submitted successfully!');
            setError(null);
        } catch (err) {
            setError('Failed to submit review. Please try again.');
            setSuccess(null);
        }
    };

    return (
        <div>
            <h2>Review Your Ride</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Email:</label>
                    <input
                        type="email"
                        value={userEmail}
                        onChange={(e) => setUserEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Rating:</label>
                    <input
                        type="number"
                        value={rating}
                        onChange={(e) => setRating(Number(e.target.value))}
                        min="1"
                        max="5"
                        required
                    />
                </div>
                <div>
                    <label>Comment:</label>
                    <textarea
                        value={comment}
                        onChange={(e) => setComment(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Submit Review</button>
            </form>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {success && <p style={{ color: 'green' }}>{success}</p>}
        </div>
    );
};

export default ReviewRide;
