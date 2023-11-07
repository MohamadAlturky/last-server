import "./card.css";
import "bootstrap/dist/css/bootstrap.min.css";
function Card(props: ICardProps) {
  return (
    <div className="card">
      <div
        className="card-image"
        style={{
          background: `url(${props.imageUrl}), lightgray 50% / cover no-repeat`,
        }}
      ></div>
      <div className="card-name">{props.imageName}</div>
    </div>
  );
}

export default Card;
