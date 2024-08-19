import React, { useEffect, useState } from 'react';
import { getCompletedRides } from '../../../services/Rides/GetCompletedRides/GetCompletedRides';

function GetCompletedRidesComponent() {
    const [rides, setRides] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchRides = async () => {
            try {
                const data = await getCompletedRides();
                setRides(data);
            } catch (error) {
                setError('Failed to load completed rides.');
            }
        };

        fetchRides();
    }, []);

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <h2>Completed Rides</h2>
            <ul>
                {rides.map(ride => (
                    <li key={ride.rideId}>
                        <p><strong>Start Address:</strong> {ride.startAddress}</p>
                        <p><strong>End Address:</strong> {ride.endAddress}</p>
                        <p><strong>Price:</strong> {ride.price}</p>
                        <p><strong>Predicted Time:</strong> {ride.predictedTime}</p>
                        <p><strong>Waiting Time:</strong> {ride.waitingTime}</p>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default GetCompletedRidesComponent;
