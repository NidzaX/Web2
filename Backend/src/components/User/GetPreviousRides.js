import React, { useEffect, useState } from 'react';
import { getPreviousRides } from '../../services/Rides/GetPreviousRides/GetPreviousRides';

const PreviousRides = () => {
    const [rides, setRides] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchRides = async () => {
            try {
                const token = localStorage.getItem('token'); // Assuming you store JWT in localStorage
                const data = await getPreviousRides(token);
                setRides(data);
            } catch (err) {
                setError('Failed to fetch rides');
            }
        };

        fetchRides();
    }, []);

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <h2>Previous Rides</h2>
            <ul>
                {rides.map((ride) => (
                    <li key={ride.rideId}>
                        From: {ride.startAddress} To: {ride.endAddress} - Price: ${ride.price} - Predicted Time: {ride.predictedTime} mins - Waiting Time: {ride.waitingTime} sec
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default PreviousRides;
