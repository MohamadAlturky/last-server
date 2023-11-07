import { CSVLink } from "react-csv";
interface CsvProps {
  data: any;
  label: string;
  headers: { label: string; key: string }[];
  fileName: string;
}
export default function Csv(props: CsvProps) {
  return (
    <>
      <CSVLink
        className="text-for-download"
        headers={props.headers}
        data={props.data}
        filename={props.fileName}
      >
        {props.label}
      </CSVLink>
    </>
  );
}
