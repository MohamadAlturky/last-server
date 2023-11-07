import mealTypes from "../configurations/mealTypes.json";
import "./mealsTable.css";
import { styled } from "@mui/material/styles";
import { useCookies } from "react-cookie";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { useEffect, useState } from "react";
import Result from "../models/Result";
import ClientContext from "../contexts/api/ClientContext";
import Swal from "sweetalert2";
import { motion } from "framer-motion";
import { OutletContextType } from "../authentication/models/OutletContextType";
import { useOutletContext } from "react-router-dom";

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
  "&:last-child td, &:last-child th": {
    border: 0,
  },
}));

export default function PricingPage() {
  const { outLetProps } = useOutletContext<OutletContextType>();
  const [data, setData] = useState<PricingResponse[]>();
  const context = new ClientContext(outLetProps.error401Handler);
  const [cookies] = useCookies(["jwt"]);
  let token = cookies.jwt;

  useEffect(() => {
    context
      .get<Result<PricingResponse[]>>(
        "api/Pricing/GetPricingForCustomer",
        token
      )
      .then((response) => {
        setData(response.data.value);
        console.log(response.data);
      })
      .catch((error) => {
        Swal.fire({
          title: "خطأ",
          text: error.status.toString(),
          icon: "warning",
          confirmButtonText: "حسناً",
        });
      });
  }, []);

  return (
    <>
      <div>
        {/* {!data && <Loader />} */}
        {data && (
          <motion.div
            initial={{ height: 0 }}
            animate={{ height: "expanded" ? "auto" : 0 }}
            transition={{ duration: 1, ease: "easeInOut" }}
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
                      <div className="header-table-title">السعر</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">نوع الوجبة</div>
                    </StyledTableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data?.map((row) => (
                    <StyledTableRow key={row.id}>
                      <StyledTableCell align="center">
                        {row.price}
                      </StyledTableCell>
                      <StyledTableCell align="center">
                        {
                          mealTypes.types.find(
                            (type) => type.id == row.mealType
                          )?.value
                        }
                      </StyledTableCell>
                    </StyledTableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </motion.div>
        )}
      </div>
    </>
  );
}
