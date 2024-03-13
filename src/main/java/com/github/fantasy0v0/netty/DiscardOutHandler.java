package com.github.fantasy0v0.netty;

import io.netty.buffer.ByteBuf;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.ChannelOutboundHandlerAdapter;
import io.netty.handler.codec.MessageToByteEncoder;

public class DiscardOutHandler extends MessageToByteEncoder<ByteBuf> {

  @Override
  protected void encode(ChannelHandlerContext ctx, ByteBuf byteBuf, ByteBuf byteBuf2) throws Exception {
    byteBuf2.writeBytes(byteBuf);
  }
}
