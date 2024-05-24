import React, { useEffect } from "react";
import {
    Navbar,
    NavbarBrand,
    NavbarContent,
    NavbarItem,
    NavbarMenuToggle,
    NavbarMenu,
    NavbarMenuItem,
    Link,
    Button,
} from "@nextui-org/react";

export const NavBarMain = ({ active }) => {
    const [isMenuOpen, setIsMenuOpen] = React.useState(false);
    const [activeItem, setActiveItem] = React.useState();

    useEffect(() => {
        setActiveItem(active);
    }, []);

    const menuItems = ["Accueil", "Logements", "Réservations", "Log Out"];

    return (
        <>
            <Navbar
                shouldHideOnScroll
                onMenuOpenChange={setIsMenuOpen}
                isBordered
                maxWidth="full"
            >
                <NavbarContent>
                    <NavbarMenuToggle
                        aria-label={isMenuOpen ? "Close menu" : "Open menu"}
                        className="sm:hidden"
                    />
                    <NavbarBrand>
                        <p className="font-bold text-inherit">RBNB</p>
                    </NavbarBrand>
                </NavbarContent>

                <NavbarContent
                    className="hidden sm:flex gap-4"
                    justify="center"
                >
                    <NavbarItem isActive={activeItem === 1}>
                        <Link
                            onClick={() => setActiveItem(1)}
                            color="foreground"
                            href="accueil"
                        >
                            Accueil
                        </Link>
                    </NavbarItem>
                    <NavbarItem isActive={activeItem === 2}>
                        <Link
                            onClick={() => setActiveItem(2)}
                            color="foreground"
                            href="logements"
                        >
                            Logements
                        </Link>
                    </NavbarItem>
                    <NavbarItem isActive={activeItem === 3}>
                        <Link
                            onClick={() => setActiveItem(3)}
                            color="foreground"
                            href="reservations"
                        >
                            Réservations
                        </Link>
                    </NavbarItem>
                </NavbarContent>
                <NavbarContent justify="end">
                    <NavbarItem className="hidden lg:flex">
                        <Link href="#">Login</Link>
                    </NavbarItem>
                    <NavbarItem>
                        <Button
                            as={Link}
                            color="primary"
                            href="#"
                            variant="flat"
                        >
                            Sign Up
                        </Button>
                    </NavbarItem>
                </NavbarContent>
                <NavbarMenu>
                    {menuItems.map((item, index) => (
                        <NavbarMenuItem key={`${item}-${index}`}>
                            <Link
                                color={
                                    index === 2
                                        ? "primary"
                                        : index === menuItems.length - 1
                                        ? "danger"
                                        : "foreground"
                                }
                                className="w-full"
                                href="#"
                                size="lg"
                            >
                                {item}
                            </Link>
                        </NavbarMenuItem>
                    ))}
                </NavbarMenu>
            </Navbar>
        </>
    );
};
