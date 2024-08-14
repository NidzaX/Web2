import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getUserData } from '../../services/Users/GetUser/GetUser';
import { Buffer } from 'buffer';

const GetUserComponent = () => {
    const { userId } = useParams();
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const data = await getUserData(userId);
                console.log('User Data:', data); // Debugging line
                setUser(data);
            } catch (err) {
                setError(err.message || 'Error fetching user data');
            } finally {
                setLoading(false);
            }
        };

        fetchUserData();
    }, [userId]);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    if (!user) {
        return <div>No user data available</div>;
    }

    // var base64Image = Convert.ToBase64String(Member_Picture);
    const pictureSrc = user.file ? `data:image/png;base64,${user.file}` : null;
    // const pictureSrc = user.file ? `data:image/jpeg;base64,${btoa(String.fromCharCode(...new Uint8Array(user.file)))}`  : null;
    console.log(pictureSrc)
    return (
        <div>
            <h1>User Information</h1>
            <p><strong>Username:</strong> {user.username || 'Not provided'}</p>
            <p><strong>First Name:</strong> {user.firstName || 'Not provided'}</p>
            <p><strong>Last Name:</strong> {user.lastName || 'Not provided'}</p>
            <p><strong>Email:</strong> {user.email || 'Not provided'}</p>
            <p><strong>Username:</strong> {user.username || 'Not provided'}</p>
            <p><strong>Address:</strong> {user.address || 'Not provided'}</p>
            <p><strong>Birthday:</strong> {user.birthday ? new Date(user.birthday).toDateString() : 'Not provided'}</p>
            {pictureSrc && (
                <div>
                    <strong>Profile Picture:</strong>
                    <img src={pictureSrc} alt="Profile" style={{ width: '100px', height: '100px' }} />
                </div>
            )}
        </div>
    );
};

export default GetUserComponent;
