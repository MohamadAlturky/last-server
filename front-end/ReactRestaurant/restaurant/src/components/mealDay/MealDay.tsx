import { styled } from "@mui/material/styles";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { useContext } from "react";
import "./mealDay.css";
import { ResourceContext } from "../../contexts/resource/ResourceContext";
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
interface MealDayProps {
  meals: WeeklyMeals | undefined;
  weekNumber: number;
}
export default function MealDay(props: MealDayProps) {
  const resources = useContext(ResourceContext);
  return (
    <>
      <div className="d-flex justify-content-end">
        <div className="week-header">
          {resources.Weeks.find((week) => week.id == props.weekNumber)?.name}
        </div>
      </div>
      <div className="table-container-for-meals-entries">
        <TableContainer component={Paper}>
          <Table sx={{ minWidth: 700 }} aria-label="customized table">
            <TableHead>
              <TableRow>
                <StyledTableCell align="center">
                  <div className="header-table-title">العشاء</div>
                </StyledTableCell>
                <StyledTableCell align="center">
                  <div className="header-table-title">الصحن</div>
                </StyledTableCell>

                <StyledTableCell align="center">
                  <div className="header-table-title">الوجبة</div>
                </StyledTableCell>
                <StyledTableCell align="center">
                  <div className="header-table-title">الفطور</div>
                </StyledTableCell>
                <StyledTableCell align="center">
                  <div className="header-table-title">التاريخ</div>
                </StyledTableCell>
                <StyledTableCell align="center">
                  <div className="header-table-title">اليوم</div>
                </StyledTableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {props.meals?.dailies.map((row, ind) => (
                <StyledTableRow key={ind}>
                  <>
                    <StyledTableCell align="center">
                      {row.meals.find((e) => e.type == "Dinner")?.name ? (
                        <div className="d-flex justify-content-center">
                          <div className="meal-found">
                            {row.meals.find((e) => e.type == "Dinner")?.name}
                          </div>
                        </div>
                      ) : (
                        <div className="d-flex justify-content-center">
                          <div className="no-meal-found"></div>
                        </div>
                      )}
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      {row.meals.find((e) => e.type == "Plate")?.name ? (
                        <div className="d-flex justify-content-center">
                          <div className="meal-found">
                            {row.meals.find((e) => e.type == "Plate")?.name}
                          </div>
                        </div>
                      ) : (
                        <div className="d-flex justify-content-center">
                          <div className="no-meal-found"></div>
                        </div>
                      )}
                      {/* 
                      {row.meals.find((e) => e.type == "Plate")?.name ? (
                        row.meals.find((e) => e.type == "Plate")?.name
                      ) : (
                        <div className="d-flex justify-content-center">
                          <div className="no-meal-found"></div>
                        </div>
                      )} */}
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      {row.meals.find((e) => e.type == "NormalMeal")?.name ? (
                        <div className="d-flex justify-content-center">
                          <div className="meal-found">
                            {
                              row.meals.find((e) => e.type == "NormalMeal")
                                ?.name
                            }
                          </div>
                        </div>
                      ) : (
                        <div className="d-flex justify-content-center">
                          <div className="no-meal-found"></div>
                        </div>
                      )}
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      {row.meals.find((e) => e.type == "BreakFast")?.name ? (
                        <div className="d-flex justify-content-center">
                          <div className="meal-found">
                            {row.meals.find((e) => e.type == "BreakFast")?.name}
                          </div>
                        </div>
                      ) : (
                        <div className="d-flex justify-content-center">
                          <div className="no-meal-found"></div>
                        </div>
                      )}
                      {/* {row.meals.find((e) => e.type == "BreakFast")?.name ? (
                        row.meals.find((e) => e.type == "BreakFast")?.name
                      ) : (
                        <div className="d-flex justify-content-center">
                          <div className="no-meal-found"></div>
                        </div>
                      )} */}
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      {row.atDay}
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      {resources.DaysOfWeek[ind]}
                    </StyledTableCell>
                  </>
                </StyledTableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </div>
    </>
  );
}
