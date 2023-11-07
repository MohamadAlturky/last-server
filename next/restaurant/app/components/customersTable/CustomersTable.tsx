import "./customersTable.css";
import { styled } from "@mui/material/styles";
import { useCookies } from "react-cookie";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { useContext, useEffect, useState } from "react";
import Result from "../../models/Result";
import ClientContext from "../../contexts/api/ClientContext";
import { motion } from "framer-motion";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import { useLocation, useOutletContext } from "react-router-dom";
import ReactPaginate from "react-paginate";
import { ResourceContext } from "../../contexts/resource/ResourceContext";
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
  // hide last border
  "&:last-child td, &:last-child th": {
    border: 0,
  },
}));

export default function CustomersTable() {
  const resources = useContext(ResourceContext);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const [data, setData] = useState<CustomersInformationPage>();
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
  useEffect(() => {
    context
      .get<Result<CustomersInformationPage>>(
        "api/Customers/GetCustomersPage/" + pageNumber,
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
        (data.customers.length == 0 && (
          <div className="d-flex justify-content-center">
            لا يوجد معلومات متاحة حالياً حاول لاحقاً
          </div>
        ))}
      {data && data.customers.length != 0 && (
        <>
          <motion.div
            initial={{ height: 0 }}
            animate={{ height: "expanded" ? "auto" : 0 }}
            transition={{
              duration: data.customers.length / 8,
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
                      <div className="header-table-title">الرصيد</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">البريد</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">الفئة</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">الاسم</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">الرقم الذاتي</div>
                    </StyledTableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data.customers.map((row) => (
                    <StyledTableRow key={row.id}>
                      <StyledTableCell align="center">
                        {row.balance}
                      </StyledTableCell>
                      <StyledTableCell align="center">
                        {row.hiastMail}
                      </StyledTableCell>
                      <StyledTableCell align="center">
                        {
                          resources.CustomersTypes.find(
                            (type) => type.id === row.category
                          )?.name
                        }
                      </StyledTableCell>
                      <StyledTableCell align="center">
                        {row.firstName + " " + row.lastName}
                      </StyledTableCell>
                      <StyledTableCell
                        component="th"
                        scope="row"
                        align="center"
                      >
                        {row.serialNumber}
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
