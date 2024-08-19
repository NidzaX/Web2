import React, { createContext, useState, useEffect } from 'react';

export const TimerContext = createContext();

export const TimerProvider = ({ children }) => {
    const [waitingTime, setWaitingTime] = useState(null);

    useEffect(() => {
        if (waitingTime !== null && waitingTime > 0) {
            const timer = setTimeout(() => {
                setWaitingTime(waitingTime - 1);
            }, 1000);

            return () => clearTimeout(timer); // Clean up the timer on unmount or update
        }
    }, [waitingTime]);

    return (
        <TimerContext.Provider value={{ waitingTime, setWaitingTime }}>
            {children}
        </TimerContext.Provider>
    );
};
