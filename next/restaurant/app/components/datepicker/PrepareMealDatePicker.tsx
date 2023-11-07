import dayjs, { Dayjs } from "dayjs";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { StaticDatePicker } from "@mui/x-date-pickers/StaticDatePicker";
import { useState } from "react";
import Swal from "sweetalert2";
import "./prepareMealDatePicker.css";
// import prepareIcon from "../../assets/dinner-prepare.svg";
import datepicker from "../../assets/date-picker.svg";
interface PickerProps {
  passSelectedDate: (dayjs: Dayjs | null) => void;
}
export default function PrepareMealDatePicker(props: PickerProps) {
  const [date, setDate] = useState<Dayjs | null>(dayjs());
  // console.log(date);

  const handleSubmit = () => {
    props.passSelectedDate(date);
  };
  return (
    <>
      <div className="container">
        <div className="d-flex flex-column justify-content-center align-items-center mt-5">
          <div className="date-picker-form">
            <div className="date-picker-header m-3">
              <div className="date-picker-label">اختر تاريخاً معيناً</div>
            </div>
            <input className="d-none" />
            <LocalizationProvider dateAdapter={AdapterDayjs}>
              <StaticDatePicker
                defaultValue={dayjs()}
                className="date-picker"
                disablePast
                // disableOpenPicker
                onAccept={() => {
                  Swal.fire({
                    title: "خطأ",
                    text: "",
                    icon: "question",
                    confirmButtonText: "فهمت",
                  });
                }}
                onChange={(newDate) => setDate(newDate)}
              />
            </LocalizationProvider>
            <div className="datepicker-icon">
              <img src={datepicker} alt="" />
            </div>
            <div className="btn-container-submit-date">
              <button
                className="button-for-date-picker-submit"
                onClick={handleSubmit}
              >
                <b className="text-white">الذهاب لليوم المختار</b>
              </button>{" "}
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

{
  /* <div className="d-flex justify-content-center">
            <div>{date && date.date()}/</div>
            <div>{date && date.month()}/</div>
            <div>{date && date.year()}/</div>
          </div> */
}
