import React, { useState } from 'react';
import { createRide } from '../../services/Rides/CreateRide/CreateRide';
import { reserveRide } from '../../services/Rides/ReserveRide/ReserveRide';

const CreateRide = () => {
    const [startAddress, setStartAddress] = useState('');
    const [endAddress, setEndAddress] = useState('');
    const [price, setPrice] = useState(null);
    const [predictedTime, setPredictedTime] = useState(null);
    const [rideId, setRideId] = useState(null); // Store the rideId
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(null);
    const [waitingTime, setWaitingTime] = useState(null);
    const [isProcessing, setIsProcessing] = useState(false);

    const getUserIdFromToken = () => {
        const token = localStorage.getItem('token');
        if (!token) return null;

        try {
            const base64Url = JSON.parse(token).accessToken.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = decodeURIComponent(atob(base64).split('').map((c) => {
                return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
            }).join(''));
            const payload = JSON.parse(jsonPayload);
            return payload.Id;
        } catch (error) {
            console.error('Error decoding token:', error);
            return null;
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const token = localStorage.getItem('token');
        const userId = getUserIdFromToken();

        if (!userId) {
            setError('User ID not found. Please log in again.');
            return;
        }

        const rideData = {
            userId,
            startAddress: { value: startAddress },
            endAddress: { value: endAddress },
        };

        try {
            const result = await createRide(rideData, JSON.parse(token).accessToken);
            setRideId(result.id);
            setPrice(result.price.value);
            setPredictedTime(result.predictedTime.value);
            setSuccess('Ride created successfully!');
            setError(null);
        } catch (err) {
            setError(err.message || 'Error creating ride');
            setSuccess(null);
        }
    };

    const handleReserveClick = async () => {
        setIsProcessing(true);
        setError(null);

        try {
            const token = localStorage.getItem('token');
            const reserveResult = await reserveRide(rideId, JSON.parse(token).accessToken);
            setWaitingTime(reserveResult.waitingTime);
        } catch (err) {
            setError('Failed to reserve ride. Please try again.');
        } finally {
            setIsProcessing(false);
        }
    };

    return (
        <div>
            <h2>Create Ride</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Start Address:</label>
                    <input
                        type="text"
                        value={startAddress}
                        onChange={(e) => setStartAddress(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>End Address:</label>
                    <input
                        type="text"
                        value={endAddress}
                        onChange={(e) => setEndAddress(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Create Ride</button>
            </form>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {success && <p style={{ color: 'green' }}>{success}</p>}
            {price !== null && <p>Price: ${price.toFixed(2)}</p>}
            {predictedTime !== null && <p>Predicted Time: {predictedTime} minutes</p>}

            {/* Show the Reserve Ride button if ride is successfully created */}
            {rideId && (
                <button onClick={handleReserveClick} disabled={isProcessing}>
                    {isProcessing ? 'Processing...' : 'Reserve Ride'}
                </button>
            )}

            {waitingTime !== null && <p>Waiting Time: {waitingTime} minutes</p>}
        </div>
    );
};

export default CreateRide;
