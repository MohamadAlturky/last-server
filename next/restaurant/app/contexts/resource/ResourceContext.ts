import { createContext } from "react";
import Resource from "../../resources/Resource";
import ArabicResource from "../../resources/Arabic/puplicContent.json";

export const ResourceContext = createContext<Resource>(ArabicResource);
