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
import { useOutletContext } from "react-router-dom";
import ReactPaginate from "react-paginate";
import "./accountTransactionsPage.css";
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

export default function AccountTransactionsPage() {
  const resources = useContext(ResourceContext);
  const [data, setData] = useState<AccountTransactions>();
  const [pageNumber, setPageNumber] = useState<number>(1);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const context = new ClientContext(outLetProps.error401Handler);
  const [cookies] = useCookies(["jwt"]);
  let token = cookies.jwt;
  function PageChange(selected: number) {
    console.log("selected");
    console.log(selected);

    setPageNumber(selected + 1);
  }
  useEffect(() => {
    context
      .get<Result<AccountTransactions>>(
        "api/Customers/AccountTransactionsPage/" + pageNumber,
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
        (data.accountTransactions.length == 0 && (
          <div className="d-flex justify-content-center">
            لا يوجد معلومات متاحة حالياً حاول لاحقاً
          </div>
        ))}
      {data && data.accountTransactions.length != 0 && (
        <>
          <motion.div
            initial={{ height: 0 }}
            animate={{ height: "expanded" ? "auto" : 0 }}
            transition={{
              duration: data.accountTransactions.length / 8,
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
                    {/* <StyledTableCell align="center">
                      <div className="header-table-title">رقم المنفذ</div>
                    </StyledTableCell> */}
                    <StyledTableCell align="center">
                      <div className="header-table-title">التاريخ</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">القيمة</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">العملية</div>
                    </StyledTableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data?.accountTransactions.map((row, ind) => (
                    <StyledTableRow key={ind}>
                      {/* <StyledTableCell align="center">{row.by}</StyledTableCell> */}
                      <StyledTableCell align="center">
                        {row.createdAtDay + " " + row.date}
                      </StyledTableCell>
                      <StyledTableCell align="center">
                        {row.value}
                      </StyledTableCell>
                      <StyledTableCell align="center">
                        {
                          resources.Transactions.find(
                            (transaction) => transaction.id == row.type
                          )?.name
                        }
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
            pageCount={data.size / 10}
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
