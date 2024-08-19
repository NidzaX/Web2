
import Header from "../components/general/Header";
import { Outlet } from "react-router-dom";
import { TimerProvider } from "../components/general/Context";

function HomePage(){
    return (
    <>
        <TimerProvider>
            <Header />
            <Outlet />
        </TimerProvider>
    </>

    )
}
export default HomePage;