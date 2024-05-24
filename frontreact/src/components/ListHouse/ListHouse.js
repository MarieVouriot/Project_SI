import { useEffect, useState } from "react";
import axios from "axios";
import s from "./ListHouse.module.css";
import { Card, CardBody, CardFooter } from "@nextui-org/react";

export default function ListHouse() {
    const [listHousing, setListHousing] = useState();
    const [isDetail, setIsDetail] = useState(false);
    const [detailItem, setDetailItem] = useState(0);

    useEffect(() => {
        axios
            .get("https://localhost:7194/api/Housing/getHousings")
            .then((res) => {
                setListHousing(res.data);
            });
    }, []);

    return (
        <>
            {isDetail && (
                <div className="items-center w-full h-full">
                    <div
                        id={`${s["default-modal"]}`}
                        tabindex="-1"
                        aria-hidden="true"
                        className="overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full"
                    >
                        <div className="relative p-4 w-full h-auto top-1/3 max-w-2xl max-h-full m-auto ">
                            <div className="relative bg-white rounded-lg shadow dark:bg-gray-700">
                                <div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
                                    <h3 className="text-xl font-semibold text-gray-900 dark:text-white">
                                        {detailItem.description}
                                    </h3>
                                    <button
                                        type="button"
                                        className="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
                                        onClick={() => setIsDetail(false)}
                                    >
                                        <svg
                                            className="w-3 h-3"
                                            aria-hidden="true"
                                            xmlns="http://www.w3.org/2000/svg"
                                            fill="none"
                                            viewBox="0 0 14 14"
                                        >
                                            <path
                                                stroke="currentColor"
                                                stroke-linecap="round"
                                                stroke-linejoin="round"
                                                stroke-width="2"
                                                d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"
                                            />
                                        </svg>
                                        <span className="sr-only">
                                            Close modal
                                        </span>
                                    </button>
                                </div>
                                <div className="p-4 md:p-5 space-y-4">
                                    <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                                        {detailItem.description}
                                    </p>
                                </div>
                                <div className="flex items-center p-4 md:p-5 border-t border-gray-200 rounded-b dark:border-gray-600">
                                    <button
                                        data-modal-hide="default-modal"
                                        type="button"
                                        className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
                                    >
                                        Réserver
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            )}

            <div className="gap-16 grid grid-cols-2 sm:grid-cols-3 m-24 mt-0">
                {listHousing &&
                    listHousing.map((item, index) => (
                        <Card
                            shadow="sm"
                            key={index}
                            isPressable
                            onPress={() => {
                                setIsDetail(true);
                                setDetailItem(item);
                            }}
                            className="bg-gray-100 hover:bg-gray-200 cursor-pointer h-64 justify-center rounded-xl w-full"
                        >
                            <CardBody className="overflow-visible p-0">
                                <div className="bg-gray-50 w-max"></div>
                            </CardBody>
                            <CardFooter className="text-small justify-between bg-gray-200 h-20">
                                <b>{item.description}</b>
                                <p className="text-default-500">
                                    {item.address}
                                </p>
                            </CardFooter>
                        </Card>
                    ))}
            </div>
        </>
    );
}
