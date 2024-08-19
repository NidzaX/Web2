// src/components/AllRides.js
import React, { useEffect, useState } from 'react';
import { getAllRides } from '../../../services/Rides/GetAllRides/GetAllRides';
import { useNavigate } from 'react-router-dom';

const AllRides = () => {
  const [rides, setRides] = useState([]);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchRides = async () => {
      try {
        const token = localStorage.getItem('jwtToken'); // Assuming you store the JWT token in localStorage
        const ridesData = await getAllRides(token);
        setRides(ridesData);
      } catch (err) {
        setError(err);
      }
    };

    fetchRides();
  }, []);

  if (error) {
    return <p>Error: {error}</p>;
  }

  return (
    <div>
      <h2>All Rides</h2>
      {rides.length > 0 ? (
        <table>
          <thead>
            <tr>
              <th>Ride ID</th>
              <th>User ID</th>
              <th>Driver ID</th>
              <th>Start Address</th>
              <th>End Address</th>
              <th>Price</th>
              <th>Predicted Time</th>
              <th>Waiting Time</th>
            </tr>
          </thead>
          <tbody>
            {rides.map((ride) => (
              <tr key={ride.rideId}>
                <td>{ride.rideId}</td>
                <td>{ride.userId}</td>
                <td>{ride.driverId}</td>
                <td>{ride.startAddress}</td>
                <td>{ride.endAddress}</td>
                <td>{ride.price}</td>
                <td>{ride.predictedTime}</td>
                <td>{ride.waitingTime}</td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p>No rides found.</p>
      )}
    </div>
  );
};

export default AllRides;
