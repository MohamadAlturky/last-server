import { styled } from "@mui/material/styles";
import { useCookies } from "react-cookie";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import url from "../configurations/apiConfiguration.json";
import "./mealsTable.css";
import mealTypes from "../configurations/mealTypes.json";
import { useEffect, useState } from "react";
import Result from "../models/Result";
import ClientContext from "../contexts/api/ClientContext";
import { motion } from "framer-motion";
import { OutletContextType } from "../authentication/models/OutletContextType";
import { Link, useLocation, useOutletContext } from "react-router-dom";
import ReactPaginate from "react-paginate";
import IResult from "../models/IResult";
import Swal from "sweetalert2";
const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    // backgroundColor: "#666",
    // backgroundColor: "#E4801E",
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
  // hide last border
  "&:last-child td, &:last-child th": {
    border: 0,
  },
}));

export default function MealsTable() {
  const { outLetProps } = useOutletContext<OutletContextType>();
  const [data, setData] = useState<MealsInformationPage>();
  const context = new ClientContext(outLetProps.error401Handler);
  const [cookies] = useCookies(["jwt"]);
  const locaiton = useLocation();
  locaiton.state = { askdj: "sad" };
  let token = cookies.jwt;
  const [pageNumber, setPageNumber] = useState<number>(1);
  function PageChange(selected: number) {
    console.log("selected");
    console.log(selected);

    setPageNumber(selected + 1);
  }
  const handleDelete = (id: number) => {
    Swal.fire({
      title: "هل متأكد من حذف الوجبة من النظام؟",
      showCancelButton: true,
      cancelButtonText: "إلغاء الطلب",
      confirmButtonText: "المتابعة",
    }).then((result) => {
      if (result.isConfirmed) {
        context
          .delete<IResult>("api/Meals/DeleteMealInformation/" + id, token)
          .then((response) => {
            console.log(response.data);
            if (data) {
              let newdata: MealsInformationPage = {
                count: data?.count - 1,
                mealsInformation: data?.mealsInformation.filter(
                  (item) => item.id != id
                ),
              };
              setData(newdata);
            }
          });
      }
    });
  };

  useEffect(() => {
    context
      .get<Result<MealsInformationPage>>(
        "api/Meals/GetMealsPage/" + pageNumber,
        token
      )
      .then((response) => {
        setData(response.data.value);
        console.log(response.data);
      });
  }, [pageNumber]);

  return (
    <>
      {!data ||
        (data.mealsInformation.length == 0 && (
          <div className="d-flex justify-content-center">
            لا يوجد معلومات متاحة حالياً حاول لاحقاً
          </div>
        ))}
      {data && data.mealsInformation.length != 0 && (
        <>
          <motion.div
            initial={{ height: 0 }}
            animate={{ height: "expanded" ? "auto" : 0 }}
            transition={{
              duration: data.mealsInformation.length / 8,
              ease: "easeInOut",
            }}
            className="mt-5"
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
                  {data.mealsInformation.map((row) => (
                    <StyledTableRow key={row.id}>
                      <StyledTableCell align="center">
                        <div
                          className="cancel btn me-2 scale-smaller delete-color"
                          onClick={() => {
                            handleDelete(row.id);
                          }}
                        >
                          حذف
                        </div>

                        <Link
                          to={{
                            pathname: "/EditMeal/" + row.id.toString(),
                          }}
                          className="btn cancel update-color scale-smaller"
                        >
                          تعديل
                        </Link>
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
          <ReactPaginate
            previousLabel={"السابق"}
            nextLabel={"التالي"}
            pageCount={data.count / 5}
            onPageChange={({ selected }) => PageChange(selected)}
            containerClassName="d-flex container-for-pagination"
            disabledClassName="disabled-for-pagination"
            activeClassName="active-for-pagination"
          ></ReactPaginate>
        </>
      )}
    </>
  );
}
