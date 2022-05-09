import React from 'react';
import './styles/style.scss';
import MainPage from './pages/MainPage';

import {
    BrowserRouter,
    Routes,
    Route,
} from "react-router-dom";

import CheckAuth from "./utils/CheckAuth";

const SignInPage = React.lazy(() => import ('./pages/SignInPage'));
const SignUpPage = React.lazy(() => import ('./pages/SignUpPage'));
const ProfilePage = React.lazy(() => import ('./pages/ProfilePage'));
const PublicEmployerProfile = React.lazy(() => import ('./pages/PublicEmployerProfile'));

function App() {
  return (
    <div>
        <React.Suspense fallback={<>Loading</>}>
            <BrowserRouter>
                <Routes>
                    <Route path='/' element={<MainPage />} />
                    <Route path='/signin' element={<SignInPage />} />
                    <Route path='/signup' element={<SignUpPage />} />
                    <Route path='/profile' element={<CheckAuth><ProfilePage /></CheckAuth>} />
                    <Route path='/company/:id' element={<CheckAuth><PublicEmployerProfile /></CheckAuth>} />
                </Routes>
            </BrowserRouter>
        </React.Suspense>
    </div>
  );
}

export default App;
