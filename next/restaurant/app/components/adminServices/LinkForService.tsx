import { useNavigate } from "react-router-dom";

export default function LinkForService(props: AdminService) {
  const navigate = useNavigate();
  let coloredBtn = "accept " + props.colorClass;
  return (
    <>
      <div className="cookie-card mb-5">
        <span className="title">{props.title}</span>
        <div className="description">{props.descritption}</div>
        <div className="actions">
          <button
            className={coloredBtn}
            onClick={() => {
              navigate(props.link);
            }}
          >
            الذهاب
          </button>
          {/* <button className="pref">تسجيل وجبة جديدة</button> */}
        </div>
      </div>
    </>
  );
}
