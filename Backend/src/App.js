import React from 'react';
import { Routes, Route } from 'react-router-dom';
import './App.css';
import Login from './components/general/Login';
import Register from './components/general/Register';
import ChangePassword from './components/general/ChangePassword';
import EditUserProfile from './components/general/EditUser';
import GetUserComponent from './components/general/Profile';
import Unauthorized from './components/general/Unauthorized';
import PrivateRoute from './components/general/PrivateRoute';
import VerifyDriverComponent from './components/admin/VerifyDriver/VerifyDriver';
import PrivateRouteA from './components/general/PrivateRouteA';
import CreateRide from './components/User/NewRide';
import HomePage from './pages/HomePage';
import ReviewRide from './components/User/AddReview';

function App() {
    return (
        <div>
            <Routes>
                <Route path='/unauthorized' element={<Unauthorized />} />
                <Route path='/' element={<PrivateRouteA><Login /></PrivateRouteA>} />
                <Route path='/login' element={<PrivateRouteA><Login /></PrivateRouteA>} />
                <Route path='/register' element={<PrivateRouteA><Register /></PrivateRouteA>} />
                <Route path="/verification" element={<VerifyDriverComponent />} />
                <Route path="Profile/:userId" element={<GetUserComponent />} />
                <Route path='/home' element={<PrivateRoute allowedRoles={['admin', 'driver', 'user']}><HomePage /></PrivateRoute>}>
                    <Route path="ChangePassword" element={<ChangePassword />} />
                    <Route path="EditProfile" element={<EditUserProfile />} />
                    <Route path="NewRide" element={<CreateRide />} />
                    <Route path="AddReview" element={<ReviewRide />} /> 
                </Route>
            </Routes>
        </div>
    );
}

export default App;
