// src/components/Driver/GetAvailableRidesComponent.js
import React, { useEffect, useState } from 'react';
import { getAvailableRides } from '../../../services/Rides/GetAvailableRides/GetAvailableRides';
import {reserveRideByDriver} from '../../../services/Rides/ReserveRideByDriver/RerserveRideByDriver';

function GetAvailableRidesComponent() {
    const [rides, setRides] = useState([]);
    const [error, setError] = useState(null);
    const [reservingRideId, setReservingRideId] = useState(null);

    useEffect(() => {
        const fetchRides = async () => {
            try {
                const data = await getAvailableRides();
                setRides(data);
            } catch (error) {
                setError('Failed to load available rides.');
            }
        };

        fetchRides();
    }, []);

    const handleReserve = async (rideId) => {
        setReservingRideId(rideId);
        try {
            await reserveRideByDriver(rideId);
            // Optionally, refetch rides to update the list or provide feedback
            alert('Ride reserved successfully!');
            setRides(rides.filter(ride => ride.rideId !== rideId)); // Remove reserved ride from the list
        } catch (error) {
            setError('Failed to reserve the ride.');
        } finally {
            setReservingRideId(null);
        }
    };

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <h2>Available Rides</h2>
            <ul>
                {rides.map(ride => (
                    <li key={ride.rideId}>
                        <p><strong>Start Address:</strong> {ride.startAddress}</p>
                        <p><strong>End Address:</strong> {ride.endAddress}</p>
                        <p><strong>Price:</strong> {ride.price}</p>
                        <p><strong>Predicted Time:</strong> {ride.predictedTime}</p>
                        <button 
                            onClick={() => handleReserve(ride.rideId)} 
                            disabled={reservingRideId === ride.rideId}
                        >
                            {reservingRideId === ride.rideId ? 'Reserving...' : 'Reserve Ride'}
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default GetAvailableRidesComponent;