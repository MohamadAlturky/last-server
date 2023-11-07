import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
} from "recharts";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import { useOutletContext } from "react-router-dom";
import { useContext, useEffect, useState } from "react";
import ClientContext from "../../contexts/api/ClientContext";
import { useCookies } from "react-cookie";
import Result from "../../models/Result";
import { ResourceContext } from "../../contexts/resource/ResourceContext";
import "./mealChartCustomerReservationsType.css";
import Csv from "../csv/Csv";
import Excel from "../../assets/excel.svg";
interface MealChartProps {
  mealId: number;
}
export default function MealChartCustomersReservations(props: MealChartProps) {
  const { outLetProps } = useOutletContext<OutletContextType>();
  const resources = useContext(ResourceContext);
  const [data, setData] = useState<ReservationsCustomerTypeReadModel[]>();
  const [mealInfo, setMealInfo] = useState<MealEntryReadModel>();
  const [normalizedData, setNormalizedData] =
    useState<ReservationsCustomerTypeReadModel[]>();
  const context = new ClientContext(outLetProps.error401Handler);
  const [cookies] = useCookies(["jwt"]);
  let token = cookies.jwt;
  const [csvData, setCsvData] = useState<any>();

  useEffect(() => {
    if (data) {
      let arr: ReservationsCustomerTypeReadModel[] = [];
      for (let i = 0; i < data.length; i++) {
        let type = resources.CustomersTypes.find(
          (resource) => resource.id == data[i].customerType
        )?.name;
        if (type != undefined) {
          let value = {
            customerType: type,
            numberOfCustomers: data[i].numberOfCustomers,
          };
          arr.push(value);
        }
      }
      setNormalizedData(arr);
    }
  }, [data]);
  useEffect(() => {
    context
      .get<Result<ReservationsCustomerTypeReadModel[]>>(
        "api/Reservations/GetStatisticAboutReservationsCustomersType/" +
          props.mealId,
        token
      )
      .then((response) => {
        console.log("response");
        console.log(response.data.value);

        setData(response.data.value);
      });
  }, []);
  console.log("normalizedData");
  console.log(normalizedData);

  function findType(customerCategory: string) {
    let type = resources.CustomersTypes.find(
      (type) => type.id == customerCategory
    )?.name;
    if (type != undefined) {
      return type;
    } else {
      return "";
    }
  }
  function findStatus(status: string) {
    let type = resources.styles.find((type) => type.id == status)?.message;
    if (type != undefined) {
      return type;
    } else {
      return "";
    }
  }
  function HandleGenerateExcel() {
    context
      .get<Result<ReservationsReadModel>>(
        "api/Reservations/GetReservationsByMealId/" + props.mealId,
        token
      )
      .then((response) => {
        console.log("response");
        console.log(response.data.value);
        setMealInfo(response.data.value.mealReadModel);
        let normalizedReservations: ReservationRecord[] = [];
        response.data.value.records.map((record) =>
          normalizedReservations.push({
            id: record.id,
            serialNumber: record.serialNumber,
            customerName: record.customerName,
            customerCategory: findType(record.customerCategory),
            status: findStatus(record.status),
          })
        );

        setCsvData(normalizedReservations);
      });
  }
  const headers = [
    { key: "id", label: "رقم الحجز" },
    { key: "serialNumber", label: "الرقم الذاتي" },
    { key: "customerName", label: "الاسم" },
    { key: "status", label: "حالة الحجز" },
    { key: "customerCategory", label: "الفئة" },
  ];
  if (csvData) {
    return (
      <>
        <div className="container">
          <div className="d-flex justify-content-center align-items-center flex-column">
            <img src={Excel} alt="" />
            <Csv
              fileName={
                mealInfo?.name
                  ? mealInfo?.name + " يوم " + mealInfo.day
                  : "حجوزات الوجبة"
              }
              label="تحميل الحجوزات"
              headers={headers}
              data={csvData}
            ></Csv>
          </div>
        </div>
      </>
    );
  }

  return (
    <div className="container d-flex justify-content-center mt-5">
      {normalizedData == undefined && <h1> لا يوجد حجوزات حتى الآن</h1>}
      {normalizedData?.length == 0 && <h1> لا يوجد حجوزات حتى الآن</h1>}
      {normalizedData && normalizedData?.length != 0 && (
        <div className="d-flex flex-column align-items-center">
          <div className="d-flex justify-content-end w-100 p-1">
            <div
              className="btn p-2 btn-primary text-for-download"
              onClick={HandleGenerateExcel}
            >
              Excel تحميل الحجوزات كملف
            </div>
          </div>
          <BarChart
            width={700}
            height={500}
            data={normalizedData}
            margin={{
              top: 5,
              right: 30,
              left: 20,
              bottom: 5,
            }}
            barSize={20}
          >
            <XAxis
              dataKey="customerType"
              arabicForm="initial"
              //   scale="point"
              padding={{ left: 10, right: 10 }}
            />
            <YAxis />
            <Tooltip />
            <Legend />
            <CartesianGrid strokeDasharray="3 3" />
            <Bar
              name="عدد الحجوزات"
              dataKey="numberOfCustomers"
              fill="#8884d8"
              background={{ fill: "#eee" }}
            />
          </BarChart>
        </div>
      )}
    </div>
  );
}
