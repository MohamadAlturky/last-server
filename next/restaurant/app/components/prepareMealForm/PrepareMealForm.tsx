import url from "../../configurations/apiConfiguration.json";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { useOutletContext } from "react-router-dom";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import mealTypes from "../../configurations/mealTypes.json";
import { useState } from "react";
import { useCookies } from "react-cookie";
import ClientContext from "../../contexts/api/ClientContext";
import Result from "../../models/Result";
import "./prepareMealForm.css";
import Swal from "sweetalert2";
import { styled } from "@mui/material";
import { motion } from "framer-motion";
import IResult from "../../models/IResult";
interface PrepareMealFormProps {
  choise: string;
  atDay: string;
  refresh: () => void;
}
const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: "#666",
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 20,
  },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({
  "&:nth-of-type(odd)": {
    backgroundColor: theme.palette.action.hover,
  },
  "&:last-child td, &:last-child th": {
    border: 0,
  },
}));
export function PrepareMealForm(props: PrepareMealFormProps) {
  const [mealName, setMealName] = useState<string>("");
  const [meals, setMeals] = useState<Meal[] | null>(null);

  const { outLetProps } = useOutletContext<OutletContextType>();
  const [cookies] = useCookies(["jwt"]);
  const clientContext = new ClientContext(outLetProps.error401Handler);

  const handleMealNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setMealName(event.target.value);
  };

  const handelPrepare = (id: number) => {
    Swal.fire({
      title: "أدخل عدد الوجبات المحضرة",
      input: "number",
      inputAttributes: {
        autocapitalize: "off",
      },
      showCancelButton: true,
      confirmButtonText: "تأكيد",
      showLoaderOnConfirm: true,
      allowOutsideClick: () => !Swal.isLoading(),
    }).then((result) => {
      if (result.isConfirmed) {
        if (!(result.value > 0)) {
          Swal.fire({
            title: "خطأ",
            text: "عذراً املأ العدد بشكل صحيح ثم حاول مرة أخرى",
            icon: "error",
            confirmButtonText: "حسناً",
          });
        } else {
          let token = cookies.jwt;
          let formData = new FormData();
          formData.append("mealId", id.toString());
          formData.append("atDay", props.atDay);
          formData.append("numberOfUnits", result.value);

          clientContext
            .post<IResult>(
              "api/Meals/PrepareMeal",
              token,
              formData,
              "multipart/form-data"
            )
            .then((response) => {
              console.log("response");
              console.log(response);
              const Toast = Swal.mixin({
                toast: true,
                position: "top-end",
                showConfirmButton: false,
                timer: 1000,
                timerProgressBar: true,
                didOpen: (toast) => {
                  toast.addEventListener("mouseenter", Swal.stopTimer);
                  toast.addEventListener("mouseleave", Swal.resumeTimer);
                },
              });

              Toast.fire({
                icon: "success",
                title: "تم",
              });
              window.sessionStorage.setItem("choise", "");
              props.refresh();
            })
            .catch((err) => {
              console.log(err);
              // Swal.fire({
              //   title: "خطأ",
              //   text: err.message,
              //   icon: "error",
              //   confirmButtonText: "اممم",
              // });
            });
          // Swal.fire({
          //   title: `${result.value}`,
          // });
        }
      }
    });
  };
  function handleGetMeals(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    if (mealName.length === 0) {
      Swal.fire({
        title: "خطأ",
        text: "عذراً املأ الاسم ثم حاول مرة أخرى",
        icon: "error",
        confirmButtonText: "حسناً",
      });
    } else {
      let token = cookies.jwt;

      clientContext
        .get<Result<Meal[]>>(
          "api/Meals/GetMealsByNameAndType/" + mealName + "/" + props.choise,
          token,
          "application/json"
        )
        .then((response) => {
          setMeals(response.data.value);
          console.log("response");
          console.log(response);
        })
        .catch((err) => {
          console.log(err);
        });
    }
  }

  return (
    <>
      <div className="container mt-5">
        <div className="d-flex justify-content-end">
          <div
            className="cancel"
            onClick={() => {
              window.sessionStorage.setItem("choise", "");
              props.refresh();
            }}
          >
            رجوع
          </div>
        </div>
        {meals && meals.length == 0 && <div>لا يوجد أي وجبة مطابقة</div>}
        {meals && meals.length != 0 && (
          <motion.div
            initial={{ height: 0 }}
            animate={{ height: "expanded" ? "auto" : 0 }}
            transition={{ duration: meals.length / 4, ease: "easeInOut" }}
            style={{
              overflow: "hidden",
              border: "1px solid #ccc",
            }}
          >
            <TableContainer component={Paper}>
              <Table sx={{ minWidth: 700 }} aria-label="customized table">
                <TableHead>
                  <TableRow>
                    <StyledTableCell align="center">
                      <div className="header-table-title">عمليات</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">الوصف</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">
                        {" "}
                        عدد السعرات الحرارية
                      </div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">نوع الوجبة</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">اسم الوجبة</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">الصورة</div>
                    </StyledTableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {meals?.map((row) => (
                    <StyledTableRow key={row.id}>
                      <StyledTableCell align="center">
                        <div className="d-flex justify-content-center">
                          <div
                            className="cancel"
                            onClick={() => handelPrepare(row.id)}
                          >
                            تحضير وجبة جديدة
                          </div>
                        </div>
                      </StyledTableCell>
                      <StyledTableCell align="center">
                        {row.description}
                      </StyledTableCell>
                      <StyledTableCell align="center">
                        {row.numberOfCalories}
                      </StyledTableCell>
                      <StyledTableCell align="center">
                        {
                          mealTypes.types.find((type) => type.id == row.type)
                            ?.value
                        }
                      </StyledTableCell>
                      <StyledTableCell align="center">
                        {row.name}
                      </StyledTableCell>
                      <StyledTableCell
                        component="th"
                        scope="row"
                        align="center"
                      >
                        <img
                          className="img-in-table"
                          src={url.baseUrl + row.imagePath}
                          alt=""
                        />
                      </StyledTableCell>
                    </StyledTableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </motion.div>
        )}
        {!meals && (
          <div className="d-flex justify-content-center mt-5">
            <div className="scale-sider"></div>
            <form className="edit-pricing-form" onSubmit={handleGetMeals}>
              <div className="create-meal-label">
                إضافة
                {mealTypes?.types.map(
                  (mealType: MealType) =>
                    props.choise == mealType.id && " " + mealType.value
                )}
              </div>

              <div className="input-container-on-meal-form position-relative">
                <input
                  className="create-meal-input"
                  type="text"
                  placeholder="اسم الوجبة"
                  dir="rtl"
                  onChange={handleMealNameChange}
                  value={mealName}
                />
              </div>

              <button type="submit" className="submit-create-meal">
                <p>البحث</p>
              </button>
            </form>
          </div>
        )}
      </div>
    </>
  );
}

// <div className="input-container-on-meal-form">
//   <select
//     value={""}
//     className="form-select select-meal-type"
//     aria-label="Default select example"
//     dir="rtl"
//   >
//     {autoComplete.map((stuff) => (
//       <option value={stuff.mealId}>{stuff.mealName}</option>
//     ))}
//   </select>
// </div>
