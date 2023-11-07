import { motion } from "framer-motion";
import { styled } from "@mui/material/styles";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { useEffect, useState } from "react";
import SignalRService from "../../services/SignalRService";
import { OutletContextType } from "../../authentication/models/OutletContextType";
import { useOutletContext } from "react-router-dom";
import ClientContext from "../../contexts/api/ClientContext";
import { useCookies } from "react-cookie";
// import { ResourceContext } from "../../contexts/resource/ResourceContext";
import Result from "../../models/Result";
import ReactPaginate from "react-paginate";
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
export default function NotificationReciever() {
  const [notificationsPage, setNotificationsPage] = useState<NotificationsPage>(
    { count: 0, notificationMessages: [] }
  );
  // const resources = useContext(ResourceContext);
  const { outLetProps } = useOutletContext<OutletContextType>();
  const context = new ClientContext(outLetProps.error401Handler);
  const [cookies] = useCookies(["jwt"]);
  const [pageNumber, setPageNumber] = useState<number>(1);

  useEffect(() => {
    SignalRService.startConnection();

    SignalRService.addNotificationListener(
      (messageSubject: string, messageContent: string, sentAt: string) => {
        let notification: NotificationMessage = {
          messageSubject: messageSubject,
          messageContent: messageContent,
          sentAt: sentAt,
          id: 0,
        };
        setNotificationsPage((prevNotifications) => {
          return {
            count: prevNotifications.count + 1,
            notificationMessages: [
              notification,
              ...prevNotifications.notificationMessages,
            ],
          };
        });
      }
    );
    return () => {
      SignalRService.stopConnection();
    };
  }, []);

  useEffect(() => {
    let token = cookies.jwt;

    context
      .get<Result<NotificationsPage>>(
        "api/Chat/GetNotificationsPage/" + pageNumber,
        token
      )
      .then((response) => {
        console.log(response);

        setNotificationsPage(response.data.value);
      });
  }, [pageNumber]);

  function PageChange(selected: number) {
    console.log("selected");
    console.log(selected);

    setPageNumber(selected + 1);
  }
  return (
    <>
      <div className="container">
        <h1 className="row d-flex justify-content-center mt-5 mb-3">
          الإشعارات
        </h1>
      </div>

      {notificationsPage.notificationMessages.length != 0 && (
        <>
          <motion.div
            initial={{ height: 0 }}
            animate={{ height: "expanded" ? "auto" : 0 }}
            transition={{
              duration: notificationsPage.notificationMessages.length / 8,
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
                      <div className="header-table-title">الوقت</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">المحتوى</div>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <div className="header-table-title">الموضوع</div>
                    </StyledTableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {notificationsPage.notificationMessages.map((row) => (
                    <StyledTableRow key={row.id}>
                      <StyledTableCell align="center">
                        {row.sentAt}
                      </StyledTableCell>
                      <StyledTableCell align="center">
                        {row.messageContent}
                      </StyledTableCell>

                      <StyledTableCell align="center">
                        {row.messageSubject}
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
            pageCount={notificationsPage.count / 5}
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
