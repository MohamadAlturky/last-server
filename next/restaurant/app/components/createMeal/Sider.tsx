import "./sider.css";
interface SiderProps {
  transformer: string;
}
function Sider(props: SiderProps) {
  let classes = props.transformer + " " + "sider-container";
  return <div className={classes}></div>;
}

export default Sider;
