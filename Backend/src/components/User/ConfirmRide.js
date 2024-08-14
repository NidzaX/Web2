import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { reserveRide } from '../../services/Rides/ReserveRide/ReserveRide';

const ReserveRideButton = () => {
    const [isProcessing, setIsProcessing] = useState(false);
    const [error, setError] = useState(null);
    const [waitingTime, setWaitingTime] = useState(null);
    const [predictedTime, setPredictedTime] = useState(null);
    const [waitingCountdown, setWaitingCountdown] = useState(null);
    const [predictedCountdown, setPredictedCountdown] = useState(null);
    const [redirectToReview, setRedirectToReview] = useState(false);

    const location = useLocation();
    const navigate = useNavigate();
    const queryParams = new URLSearchParams(location.search);
    const rideId = queryParams.get('rideId');

    const token = localStorage.getItem('token');

    const handleReserveClick = async () => {
        setIsProcessing(true);
        setError(null);

        try {
            // Reserve ride
            const reserveResult = await reserveRide(rideId, token);
            console.log('Reserve Result:', reserveResult);

            setWaitingTime(Math.ceil(reserveResult.waitingTime));
            setWaitingCountdown(Math.ceil(reserveResult.waitingTime));
            setPredictedTime(Math.ceil(reserveResult.predictedTime));
            setPredictedCountdown(Math.ceil(reserveResult.predictedTime));

            disableOtherComponents();

            // Set the redirect flag to true after successful reservation
            setRedirectToReview(true);
        } catch (error) {
            console.error('Error:', error);
            setError('Failed to process ride. Please try again.');
        } finally {
            setIsProcessing(false);
        }
    };

    // Countdown logic
    useEffect(() => {
        if (waitingCountdown !== null && waitingCountdown > 0) {
            const waitingInterval = setInterval(() => {
                setWaitingCountdown(prev => prev > 0 ? prev - 1 : 0);
            }, 1000);

            return () => clearInterval(waitingInterval);
        }
    }, [waitingCountdown]);

    useEffect(() => {
        if (predictedCountdown !== null && predictedCountdown > 0) {
            const predictedInterval = setInterval(() => {
                setPredictedCountdown(prev => prev > 0 ? prev - 1 : 0);
            }, 1000);

            return () => clearInterval(predictedInterval);
        }
    }, [predictedCountdown]);

    // Enable other components when both countdowns are done
    useEffect(() => {
        if (waitingCountdown === 0 && predictedCountdown === 0) {
            enableOtherComponents();
        }
    }, [waitingCountdown, predictedCountdown]);

    // Redirection logic
    useEffect(() => {
        if (redirectToReview && waitingCountdown === 0 && predictedCountdown === 0) {
            navigate(`/home/AddReview?rideId=${rideId}`);
        }
    }, [redirectToReview, waitingCountdown, predictedCountdown, navigate, rideId]);

    const disableOtherComponents = () => {
        const elementsToDisable = document.querySelectorAll('.disable-during-countdown');
        elementsToDisable.forEach(element => {
            element.style.pointerEvents = 'none';
            element.style.opacity = '0.5';
        });
    };

    const enableOtherComponents = () => {
        const elementsToEnable = document.querySelectorAll('.disable-during-countdown');
        elementsToEnable.forEach(element => {
            element.style.pointerEvents = 'auto';
            element.style.opacity = '1';
        });
    };

    return (
        <div>
            <button 
                onClick={handleReserveClick} 
                disabled={isProcessing || waitingCountdown !== null || predictedCountdown !== null}
            >
                {isProcessing ? 'Processing...' : 'Reserve Ride'}
            </button>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {(waitingCountdown !== null || predictedCountdown !== null) && (
                <div>
                    {waitingCountdown !== null && waitingCountdown > 0 && (
                        <p>Driver arriving in: {waitingCountdown} seconds</p>
                    )}
                    {predictedCountdown !== null && predictedCountdown > 0 && (
                        <p>Ride will take: {predictedCountdown} seconds</p>
                    )}
                </div>
            )}
        </div>
    );
};

export default ReserveRideButton;
