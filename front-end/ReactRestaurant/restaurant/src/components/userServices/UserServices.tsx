import { useContext } from "react";
import { ResourceContext } from "../../contexts/resource/ResourceContext";
import LinkForService from "../adminServices/LinkForService";
import "../adminServices/adminServices.css";

export default function UserServices() {
  const resources = useContext(ResourceContext);

  return (
    <div className="d-flex mt-5 row">
      {resources.UserServices.map((service, index) => (
        <div className="col-lg-4 col-md-6 col-sm-12 d-flex justify-content-center">
          <LinkForService
            descritption={service.descritption}
            link={service.link}
            title={service.title}
            colorClass={service.colorClass}
            key={index}
          />
        </div>
      ))}
    </div>
  );
}
