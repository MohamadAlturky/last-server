import "bootstrap/dist/css/bootstrap.css";
import "bootstrap/dist/js/bootstrap.js";
import "./css/global.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { ResourceContext } from "./contexts/resource/ResourceContext";
import ArabicResource from "./resources/Arabic/puplicContent.json";
import Requirement from "./authentication/components/Requirement";
import LogInPage from "./pages/LogIn";
import Welcome from "./pages/Welcome";
import data from "./configurations/permissions.json";
import Home from "./pages/Home";
import CreateMeal from "./components/createMeal/CreateMeal";
import EditPricing from "./components/editPricing/EditPricing";
import Register from "./components/register/Register";
import MealsTable from "./pages/MealsTable";
import PrepareMeal from "./pages/PrepareMeal";
import { IncreaseCustomersBalance } from "./components/IncreaseCustomersBalance/IncreaceCustomersBalance";
import ChangePassword from "./components/changePassword/ChangePassword";
import SendMessage from "./components/sendMessage/SendMessage";
import NotificationReciever from "./components/notifications/NotificationReciever";
import ConsumeReservations from "./components/consumeReservations/ConsumeReservations";
import FourOFour from "./pages/FourOFour";
import ForgetPassword from "./components/forgetPassword/ForgetPassword";
import AdminServices from "./components/adminServices/AdminServices";
import AccountTransactionsPage from "./components/accountTransactions/AccountTransactionsPage";
import MealSchedule from "./components/mealSchedule/MealSchedule";
import ReservationsStatus from "./components/reservationsStatus/ReservationsStatus";
import Profile from "./components/profile/Profile";
import PricingPage from "./pages/PricingPage";
import FindCustomer from "./components/findCustomer/FindCustomer";
import CallUs from "./pages/CallUs";
import EditMeal from "./components/editMeal/EditMeal";
import CustomersTable from "./components/customersTable/CustomersTable";
import UserServices from "./components/userServices/UserServices";

function App() {
  return (
    <>
      <ResourceContext.Provider value={ArabicResource}>
        <BrowserRouter>
          <Routes>
            <Route key={1} path="/login" element={<LogInPage />} />
            <Route key={3} path="/" element={<Welcome />} />
            <Route key={3} path="/CallUs" element={<CallUs />} />
            <Route
              key={4}
              path="/ForgetPassword"
              element={<ForgetPassword />}
            />

            <Route
              key={5}
              element={
                <Requirement
                  activeLink={3}
                  permission={data.permissions.NoPermissionNeeded}
                />
              }
            >
              <Route key={6} path="/MealSchedule" element={<MealSchedule />} />
            </Route>

            <Route
              element={
                <Requirement
                  activeLink={9}
                  permission={data.permissions.SeePublicContent}
                />
              }
            >
              <Route path="/Profile" element={<Profile />} />
            </Route>

            <Route
              key={7}
              element={
                <Requirement
                  activeLink={9}
                  permission={data.permissions.NoPermissionNeeded}
                />
              }
            >
              <Route
                key={8}
                path="/NotificationReciever"
                element={<NotificationReciever />}
              />
            </Route>

            <Route
              key={9}
              element={
                <Requirement
                  activeLink={1}
                  permission={data.permissions.OrderContent}
                />
              }
            >
              <Route
                key={10}
                path="/ChangePassword"
                element={<ChangePassword />}
              />
            </Route>

            <Route
              key={11}
              element={
                <Requirement
                  activeLink={4}
                  permission={data.permissions.CreateSystemInformation}
                />
              }
            >
              <Route key={12} path="/Register" element={<Register />} />
            </Route>

            <Route
              key={11}
              element={
                <Requirement
                  activeLink={0}
                  permission={data.permissions.ReadSystemInformation}
                />
              }
            >
              <Route
                key={12}
                path="/CustomersTable"
                element={<CustomersTable />}
              />
            </Route>
            <Route
              key={11}
              element={
                <Requirement
                  activeLink={1}
                  permission={data.permissions.CreateSystemInformation}
                />
              }
            >
              <Route key={12} path="/EditMeal/:id" element={<EditMeal />} />
            </Route>
            <Route
              key={11}
              element={
                <Requirement
                  activeLink={0}
                  permission={data.permissions.ReadContent}
                />
              }
            >
              <Route key={12} path="/PricingPage" element={<PricingPage />} />
            </Route>

            <Route
              key={13}
              element={
                <Requirement
                  activeLink={1}
                  permission={data.permissions.ReadSystemInformation}
                />
              }
            >
              {/* <Route key={1}
                path="/MealChartCustomersReservations"
                element={<MealChartCustomersReservations mealId={2} />}
              /> */}
              <Route key={14} path="/MealsTable" element={<MealsTable />} />
            </Route>

            <Route
              key={13}
              element={
                <Requirement
                  activeLink={1}
                  permission={data.permissions.ReadSystemInformation}
                />
              }
            >
              {/* <Route key={1}
                path="/MealChartCustomersReservations"
                element={<MealChartCustomersReservations mealId={2} />}
              /> */}
              <Route key={14} path="/FindCustomer" element={<FindCustomer />} />
            </Route>

            <Route
              key={15}
              element={
                <Requirement
                  activeLink={1}
                  permission={data.permissions.EditBalances}
                />
              }
            >
              <Route
                key={16}
                path="/IncreaseCustomersBalance"
                element={<IncreaseCustomersBalance />}
              />
            </Route>

            <Route
              key={15}
              element={
                <Requirement
                  activeLink={1}
                  permission={data.permissions.EditBalances}
                />
              }
            >
              <Route
                key={16}
                path="/IncreaseCustomerBalance"
                element={<IncreaseCustomersBalance />}
              />
            </Route>

            <Route
              key={17}
              element={
                <Requirement
                  activeLink={1}
                  permission={data.permissions.ConsumeReservations}
                />
              }
            >
              <Route
                key={18}
                path="/ConsumeReservations"
                element={<ConsumeReservations />}
              />
            </Route>

            <Route
              key={19}
              element={
                <Requirement
                  activeLink={0}
                  permission={data.permissions.CreateSystemInformation}
                />
              }
            >
              <Route
                key={21}
                path="/AdminServices"
                element={<AdminServices />}
              />
            </Route>

            <Route
              key={19}
              element={
                <Requirement
                  activeLink={0}
                  permission={data.permissions.ReadContent}
                />
              }
            >
              <Route key={21} path="/UserServices" element={<UserServices />} />
            </Route>
            <Route
              key={19}
              element={
                <Requirement
                  activeLink={0}
                  permission={data.permissions.CreateSystemInformation}
                />
              }
            >
              <Route key={22} path="/CreateMeal" element={<CreateMeal />} />
            </Route>
            <Route
              element={
                <Requirement
                  activeLink={0}
                  permission={data.permissions.CreateSystemInformation}
                />
              }
            >
              <Route key={20} path="/EditPricing" element={<EditPricing />} />
            </Route>

            <Route
              element={
                <Requirement
                  activeLink={0}
                  permission={data.permissions.CreateSystemInformation}
                />
              }
            >
              <Route key={24} path="/SendMessage" element={<SendMessage />} />
            </Route>

            <Route
              element={
                <Requirement
                  activeLink={2}
                  permission={data.permissions.CreateSystemInformation}
                />
              }
            >
              <Route key={23} path="/PrepareMeal" element={<PrepareMeal />} />
            </Route>

            <Route
              key={25}
              element={
                <Requirement
                  activeLink={4}
                  permission={data.permissions.ReadContent}
                />
              }
            >
              <Route key={26} path="/Home" element={<Home />} />
            </Route>

            <Route
              key={27}
              element={
                <Requirement
                  activeLink={2}
                  permission={data.permissions.ReadContent}
                />
              }
            >
              <Route
                key={28}
                path="/AccountTransactionsPage"
                element={<AccountTransactionsPage />}
              />
            </Route>

            <Route
              element={
                <Requirement
                  activeLink={5}
                  permission={data.permissions.ReadContent}
                />
              }
            >
              <Route
                key={30}
                path="/ReservationsStatus"
                element={<ReservationsStatus />}
              />
            </Route>

            <Route key={31} path="/*" element={<FourOFour />} />
          </Routes>
        </BrowserRouter>
      </ResourceContext.Provider>
    </>
  );
}

export default App;
