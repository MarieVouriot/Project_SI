import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import ErrorPage from "./pages/ErrorPage/ErrorPage";
import Locations from "./pages/Locations/Locations";
import Reservations from "./pages/Reservation/Reservations";
import Accueil from "./pages/Accueil/Accueil";
import { NavBarMain } from "./components/NavBarMain/NavBarMain";

const BrowserRouter = createBrowserRouter([
    {
        path: "/",
        element: (
            <>
                <NavBarMain active={"Accueil"} />
                <Accueil />
            </>
        ),
        errorElement: <ErrorPage />,
    },
    {
        path: "/accueil",
        element: (
            <>
                <NavBarMain active={"Accueil"} />
                <Accueil />
            </>
        ),
        errorElement: <ErrorPage />,
    },
    {
        path: "/locations",
        element: (
            <>
                <NavBarMain active={"Locations"} />
                <Locations />
            </>
        ),
        errorElement: <ErrorPage />,
    },
    {
        path: "/reservations",
        element: (
            <>
                <NavBarMain active={"Réservations"} />
                <Reservations />
            </>
        ),
        errorElement: <ErrorPage />,
    },
]);

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
    <React.StrictMode>
        <RouterProvider router={BrowserRouter} />
    </React.StrictMode>
);
