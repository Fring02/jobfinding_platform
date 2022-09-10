import './App.css';
import Layout from './components/layout/Layout';
import { Navigate, Route, Routes } from 'react-router-dom';
import MainSection from './components/main/MainSection';
import Profile from './components/profile/Profile';
import SignupForm from './components/auth/register/SignupForm';
import SigninForm from './components/auth/login/SigninForm';
import CompaniesList from './components/main/companies/CompaniesList';
import PositionsList from './components/main/positions/PositionsList';
import AdvertisementsSection from './components/main/advertisements/AdvertisementsSection';
import { AuthContextProvider } from './store/auth_context';
import PublishAdvertisement from './components/main/advertisements/PublishAdvertisement';
import EmailUpdateForm from './components/profile/EmailUpdateForm';
import AdvertisementPage from './components/main/advertisements/AdvertisementPage';
import ChatWindow from './components/main/chat/ChatWindow';
function App() {
  return (
    <AuthContextProvider>
      <Layout>
          <Routes>
            <Route path='/' element={<MainSection />}/>
            <Route path='/companies' element={<CompaniesList />}/>
            <Route path='/positions' element={<PositionsList />}/>
            <Route path='/profile' element={<Profile />}/>
            <Route path='/chat' element={<ChatWindow />} />
            <Route path='/email_update' element={<EmailUpdateForm />} />
            <Route path='/publish' element={<PublishAdvertisement />}/>
            <Route path='/advertisements' element={<AdvertisementsSection />}/>
            <Route path='/advertisements/:id' element={<AdvertisementPage />}/>
            <Route path='/signup' element={<SignupForm />}/>
            <Route path='/signin' element={<SigninForm />}/>
          </Routes>
      </Layout>
    </AuthContextProvider>
  );
}

export default App;
